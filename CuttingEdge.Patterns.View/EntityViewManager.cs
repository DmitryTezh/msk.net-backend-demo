using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Xml;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Extensions.Options;
using CuttingEdge.Patterns.Abstractions;
using CuttingEdge.Patterns.View.Properties;

namespace CuttingEdge.Patterns.View
{
    public class EntityViewManagerSettings
    {
        public string Catalog { get; set; }
    }

    public class EntityViewManager<TView> : IViewManager<TView> where TView : class, IView
    {
        private readonly EntityViewManagerSettings _settings;
        private readonly ConcurrentDictionary<string, TView> _views;

        public EntityViewManager(IOptions<EntityViewManagerSettings> settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            if (String.IsNullOrEmpty(settings.Value.Catalog))
            {
                throw new ArgumentNullException(nameof(settings.Value.Catalog));
            }
            if (!Directory.Exists(settings.Value.Catalog))
            {
                throw new ApplicationException(String.Format(Resources.ErrMsg_EntityViewManager_InvalidCatalog, settings.Value.Catalog));
            }

            _settings = settings.Value;
            _views = new ConcurrentDictionary<string, TView>();

            LoadAreas();
        }

        public TView GetFor<TEntity>(string aria, string route, string mode) where TEntity : class
        {
            if (aria == null)
            {
                throw new ArgumentNullException(nameof(aria));
            }
            if (route == null)
            {
                throw new ArgumentNullException(nameof(route));
            }
            if (mode == null)
            {
                throw new ArgumentNullException(nameof(mode));
            }

            var viewRef = GetReference<TEntity>(aria, route, mode);
            if (!_views.TryGetValue(viewRef, out var view))
            {
                viewRef = GetReference<TEntity>("@", route, mode);
                if (!_views.TryGetValue(viewRef, out view))
                {
                    throw new ApplicationException(String.Format(Resources.ErrMsg_EntityViewManager_ViewNotFound, viewRef));
                }
            }

            return view;
        }

        public void Save(TView view)
        {
            var serializer = new DataContractSerializer(typeof(TView));
            var xmlSettings = new XmlWriterSettings()
            {
                Indent = true
            };

            var viewPath = Path.Combine(_settings.Catalog, view.Reference, ".xml");
            using (var fileStream = new FileStream(viewPath, FileMode.Create))
            using (var xmlWriter = XmlWriter.Create(fileStream, xmlSettings))
            {
                serializer.WriteObject(xmlWriter, view);
            }
        }

        private void LoadAreas()
        {
            var arias = new DirectoryInfo(_settings.Catalog);
            var serializer = new DataContractSerializer(typeof(TView));
            arias.GetDirectories().ToList().ForEach(aria => LoadArea(aria, serializer));
        }

        private void LoadArea(DirectoryInfo aria, DataContractSerializer serializer)
        {
            aria.GetDirectories().ToList().ForEach(route => LoadRoute(route, serializer));            
        }

        private void LoadRoute(DirectoryInfo route, DataContractSerializer serializer)
        {
            route.GetFiles("*.xml").ToList().ForEach(view =>
            {
                var entityView = LoadView(view.FullName, serializer);
                if (entityView != null)
                {
                    var name = Path.GetFileNameWithoutExtension(view.Name);
                    var i = name.LastIndexOf(".");
                    var entityName = i > 0 ? name.Substring(0, i) : name;
                    var modeName = i > 0 ? name.Substring(i + 1) : "*";

                    entityView.Reference = GetReference(route.Parent.Name, route.Name, entityName, modeName);
                    _views.TryAdd(entityView.Reference, entityView);
                }
            });
        }

        private TView LoadView(string viewPath, DataContractSerializer serializer)
        {
            using (var fileStream = new FileStream(viewPath, FileMode.Open))
            using (var xmlReader = XmlReader.Create(fileStream))
            {
                try
                {
                    return (TView)serializer.ReadObject(xmlReader);
                }
                catch (Exception exception)
                {
                }
            }

            return null;
        }

        private string GetReference<TEntity>(string aria, string route, string mode)
        {
            return GetReference(aria, route, typeof(TEntity).Name, mode);
        }

        private string GetReference(string aria, string route, string entity, string mode)
        {
            return $"{aria}\\{route}\\{entity}.{mode}";
        }
    }
}

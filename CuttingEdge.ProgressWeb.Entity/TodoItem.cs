namespace CuttingEdge.ProgressWeb.Entity
{
    public class TodoItem : Domain
    {
        public int Priority { get; set; }
        public bool Completed { get; set; }
        public string Text { get; set; }
    }
}

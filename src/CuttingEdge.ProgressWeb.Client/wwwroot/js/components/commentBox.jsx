class Comment extends React.Component {
    static propTypes = {
        author: React.PropTypes.string.isRequired,
        isActive: React.PropTypes.bool
    }
    render() {
        return (
            <div className="comment" className={this.props.isActive ? "text-info" : ""}>
                <h3 className="commentAuthor">
                    {this.props.author}
                </h3>
                {this.props.children}
            </div>
        );
    }
}

class CommentList extends React.Component {
    render() {
        const commentNodes = this.props.data ?
            this.props.data.map(comment =>
                <Comment author={comment.author} key={comment.id} isActive={comment.id === this.props.activeCommentId}>
                    {comment.text}
                </Comment>
            ) : null;
        return (
            <div className="commentList">
                {commentNodes}
            </div>
        );
    }
}

class CommentForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = { author: '', text: '' };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleAuthorChange = this.handleAuthorChange.bind(this);
        this.handleTextChange = this.handleTextChange.bind(this);
    }
    handleAuthorChange(e: Event) {
        this.setState({ author: e.target.value });
    }
    handleTextChange(e: Event) {
        this.setState({ text: e.target.value });
    }
    handleSubmit(e: Event) {
        e.preventDefault();

        const author = this.state.author.trim();
        const text = this.state.text.trim();
        if (!author || !text) {
            return;
        }

        this.props.onCommentSubmit({ author: author, text: text });
        this.setState({ author: '', text: '' });
    }
    render() {
        return (
            <form className="commentForm" onSubmit={this.handleSubmit}>
                <input type="text" placeholder="Your name" value={this.state.author} onChange={this.handleAuthorChange} />
                <input type="text" placeholder="Say something..." value={this.state.text} onChange={this.handleTextChange} />
                <input type="submit" value="Post" />
            </form>
        );
    }
}

class CommentBox extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: this.props.initialData, activeComment: null };

        this.handleCommentSubmit = this.handleCommentSubmit.bind(this);
    }
    loadDataFromServer(activeComment) {
        $.getJSON(this.props.url).then(data =>
            this.setState({ data: data, activeComment: activeComment })
        );
    }
    handleCommentSubmit(comment) {
        $.post(this.props.url, comment).then(activeComment =>
            this.loadDataFromServer(activeComment));
    }
    componentDidMount() {
        this.loadDataFromServer(this.state.activeComment);
        this.timerID = window.setInterval(() => this.loadDataFromServer(this.state.activeComment), this.props.pollInterval);
    }
    componentWillUnmount() {
        window.clearTimeout(this.timerID);
    }
    render() {
        return (
            <div className="commentBox">
                <h2>Comments</h2>
                <CommentList data={this.state.data} activeCommentId={this.state.activeComment ? this.state.activeComment.id : 0} />
                <CommentForm onCommentSubmit={this.handleCommentSubmit} />
            </div>
        );
    }
}
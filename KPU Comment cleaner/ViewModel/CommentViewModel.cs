using KPU_Comment_cleaner.Model;
using KPU_Comment_cleaner.ViewModel.Base;
using SimpleJSON;

namespace KPU_Comment_cleaner.ViewModel
{
    public class CommentViewModel : ComponentViewModel
    {
        public new Comment Model { get { return base.Model as Comment; } }

        public int Index { get { return this.Model.Index; } }
        public string Ticket { get { return this.Model.Ticket; } }
        public string Id { get { return this.Model.Id; } }
        public string Template { get { return this.Model.Template; } }
        public string Pool { get { return this.Model.Pool; } }
        public string oid { get { return this.Model.oid; } }
        public string aid { get { return this.Model.aid; } }

        public override string AnotherButtonText => "Delete";

        public CommentViewModel(ConnectionViewModel owner, JSONNode json) : base(owner, json)
        {
            
        }

        protected override Component AllocateComponent(ConnectionViewModel owner, JSONNode json)
        {
            return new Comment(json);
        }

        protected override void OnAnotherWork(object obj)
        {
            this.Parent.CommentItems.Delete(this);
        }
    }
}

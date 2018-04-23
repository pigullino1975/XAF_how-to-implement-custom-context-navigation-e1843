using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace ContextNavigation.Module {
    [DefaultClassOptions, ImageName("BO_Task")]
    public class Task : BaseObject {
        public Task(Session session) : base(session) { }
        public string Description {
            get { return GetPropertyValue<string>("Description"); }
            set { SetPropertyValue<string>("Description", value); }
        }
    }
}

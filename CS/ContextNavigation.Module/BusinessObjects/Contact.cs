using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace ContextNavigation.Module {
    [DefaultClassOptions, ImageName("BO_Contact")]
    public class Contact : BaseObject {
        public Contact(Session session) : base(session) { }
        public string Name {
            get { return GetPropertyValue<string>("Name"); }
            set { SetPropertyValue<string>("Name", value); }
        }
    }
}

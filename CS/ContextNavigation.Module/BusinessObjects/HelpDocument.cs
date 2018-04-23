using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Utils;

namespace ContextNavigation.Module {
    [DefaultClassOptions, ImageName("BO_Report")]
    public class HelpDocument : BaseObject {
        public HelpDocument(Session session) : base(session) { }
        [DevExpress.Xpo.ValueConverter(typeof(TypeToStringConverter))]
        public Type ObjectType {
            get { return GetPropertyValue<Type>("ObjectType"); }
            set { SetPropertyValue<Type>("ObjectType", value); }
        }
        public string Title {
            get { return GetPropertyValue<string>("Title"); }
            set { SetPropertyValue<string>("Title", value); }
        }
        [Size(-1)]
        public string Text {
            get { return GetPropertyValue<string>("Text"); }
            set { SetPropertyValue<string>("Text", value); }
        }
    }
}
using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace ContextNavigation.Module {
    [DefaultClassOptions, ImageName("BO_Report")]
    public class HelpDocument : BaseObject {
        public HelpDocument(Session session) : base(session) { }
        [DevExpress.Xpo.ValueConverter(typeof(TypeValueConverter))]
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
    public class TypeValueConverter : ValueConverter {
        public override object ConvertToStorageType(object value) {
            return value.ToString();
        }
        public override object ConvertFromStorageType(object value) {
            if (value == null) return null;
            else return Type.GetType((string)value);
        }
        public override Type StorageType {
            get { return typeof(string); }
        }
    }
}
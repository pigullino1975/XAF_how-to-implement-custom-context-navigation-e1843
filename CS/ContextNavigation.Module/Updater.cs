using System;

using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;

namespace ContextNavigation.Module {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            HelpDocument sampleDocumentOne = ObjectSpace.FindObject<HelpDocument>(
                CriteriaOperator.Parse("Title == 'Sample topic one'"));
            if (sampleDocumentOne == null) {
                sampleDocumentOne = ObjectSpace.CreateObject<HelpDocument>();
                sampleDocumentOne.Title = "Sample topic one";
                sampleDocumentOne.ObjectType = typeof(Contact);
                sampleDocumentOne.Text = "Sample text";
                sampleDocumentOne.Save();
            }

            HelpDocument sampleDocumentTwo = ObjectSpace.FindObject<HelpDocument>(
                CriteriaOperator.Parse("Title == 'Sample topic two'"));
            if (sampleDocumentTwo == null) {
                sampleDocumentTwo = ObjectSpace.CreateObject<HelpDocument>();
                sampleDocumentTwo.Title = "Sample topic two";
                sampleDocumentTwo.ObjectType = typeof(Task);
                sampleDocumentTwo.Text = "Sample text";
                sampleDocumentTwo.Save();
            }

        }
    }
}

using System;

using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;

namespace ContextNavigation.Module {
    public class Updater : ModuleUpdater {
        public Updater(Session session, Version currentDBVersion)
            :
            base(session, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            HelpDocument sampleDocumentOne = Session.FindObject<HelpDocument>(
                CriteriaOperator.Parse("Title == 'Sample topic one'"));
            if(sampleDocumentOne == null) {
                sampleDocumentOne = new HelpDocument(Session);
                sampleDocumentOne.Title = "Sample topic one";
                sampleDocumentOne.ObjectType = typeof(Contact);
                sampleDocumentOne.Text = "Sample text";
                sampleDocumentOne.Save();
            }

            HelpDocument sampleDocumentTwo = Session.FindObject<HelpDocument>(
                CriteriaOperator.Parse("Title == 'Sample topic two'"));
            if(sampleDocumentTwo == null) {
                sampleDocumentTwo = new HelpDocument(Session);
                sampleDocumentTwo.Title = "Sample topic two";
                sampleDocumentTwo.ObjectType = typeof(Task);
                sampleDocumentTwo.Text = "Sample text";
                sampleDocumentTwo.Save();
            }

        }
    }
}

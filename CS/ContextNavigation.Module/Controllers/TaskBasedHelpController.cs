using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;

namespace ContextNavigation.Module {
    public class TaskBasedHelpController : WindowController {
        private ShowNavigationItemController navigationController;
        protected override void OnFrameAssigned() {
            UnsubscribeFromEvents();
            base.OnFrameAssigned();
            navigationController = Frame.GetController<ShowNavigationItemController>();
            if (navigationController != null)
                navigationController.NavigationItemCreated += navigationItemCreated;
        }
        void navigationItemCreated(object sender, NavigationItemCreatedEventArgs e) {
            ChoiceActionItem navigationItem = e.NavigationItem;
            IModelObjectView viewNode = ((IModelNavigationItem)e.NavigationItem.Model).View as IModelObjectView;
            if (viewNode != null) {
                ITypeInfo objectTypeInfo = XafTypesInfo.Instance.FindTypeInfo(viewNode.ModelClass.Name);
                if (objectTypeInfo != null) {
                    CriteriaOperator docCriteria = CriteriaOperator.Parse("ObjectType == ?", objectTypeInfo.Type);
                    IObjectSpace myObjectSpace = Application.CreateObjectSpace();
                    IList<HelpDocument> docs = myObjectSpace.GetObjects<HelpDocument>(docCriteria);
                    if (docs.Count > 0) {
                        ChoiceActionItem docsGroup = new ChoiceActionItem("CustomDocuments", "Task-Based Help", null) { ImageName = "BO_Report" };
                        navigationItem.Items.Add(docsGroup);
                        foreach (HelpDocument doc in docs) {
                            ViewShortcut shortcut = new ViewShortcut(typeof(HelpDocument), doc.Oid.ToString(), "HelpDocument_DetailView_FewColumns");
                            ChoiceActionItem docItem = new ChoiceActionItem(doc.Oid.ToString(), doc.Title, shortcut) { ImageName = "Navigation_Item_Report" };
                            docsGroup.Items.Add(docItem);
                        }
                    }
                }
            }
        }
        private void UnsubscribeFromEvents() {
            if (navigationController != null) {
                navigationController.NavigationItemCreated -= navigationItemCreated;
                navigationController = null;
            }
        }
        protected override void Dispose(bool disposing) {
            UnsubscribeFromEvents();
            base.Dispose(disposing);
        }
    }
}
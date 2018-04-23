using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.NodeWrappers;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;


namespace ContextNavigation.Module {
    public class TaskBasedHelpController : WindowController {
        public const string ViewId = "HelpDocument_ListView_FewColumns";
        public const string CriteriaKeyName = "Criteria";
        private string parentViewClassName;
        private ShowNavigationItemController showNavigationItemController;


        public TaskBasedHelpController() {
            this.TargetWindowType = WindowType.Main;
        }

        protected override void OnFrameAssigned() {
            UnsubscribeFromEvents();
            base.OnFrameAssigned();
            showNavigationItemController =
                Frame.GetController<ShowNavigationItemController>();
            if(showNavigationItemController != null) {
                showNavigationItemController.NavigationItemCreated +=
                    new EventHandler<NavigationItemCreatedEventArgs>(
                    showNavigationItemController_NavigationItemCreated);
                showNavigationItemController.CustomShowNavigationItem +=
                    new EventHandler<CustomShowNavigationItemEventArgs>(
                    showNavigationItemController_CustomShowNavigationItem);
            }
        }

        void showNavigationItemController_NavigationItemCreated(
            object sender, NavigationItemCreatedEventArgs e) {
            ChoiceActionItem navigationItem = e.NavigationItem;
            string viewId = e.NavigationItem.Info.GetAttributeValue(
                ShowNavigationItemController.ViewIdAttributeName);
            if(!string.IsNullOrEmpty(viewId)) {
                DictionaryNode viewInfoNode = Application.FindViewInfo(viewId);
                if(viewInfoNode != null) {
                    BaseViewInfoNodeWrapper viewInfo =
                        ViewsNodeWrapper.CreateWrapper(viewInfoNode);
                    ITypeInfo objectTypeInfo =
                        XafTypesInfo.Instance.FindTypeInfo(viewInfo.ClassName);
                    if(objectTypeInfo != null) {
                        ObjectSpace myObjectSpace = Application.CreateObjectSpace();
                        int helpDocumentsCount = myObjectSpace.GetObjectsCount(
                            typeof(HelpDocument), CriteriaOperator.Parse(
                            "ObjectType == '" + objectTypeInfo.Type.ToString() + "'"));
                        if(helpDocumentsCount != 0) {
                            ViewShortcut shortcut = new
                            ViewShortcut(typeof(HelpDocument), null, ViewId);
                            shortcut[CriteriaKeyName] = viewInfo.ClassName;
                            ChoiceActionItem DocumentsNavigationGroup =
                                new ChoiceActionItem("CustomDocuments",
                                "Task-Based Help", shortcut);
                            DocumentsNavigationGroup.Info.SetAttribute(
                                ShowNavigationItemController.GroupImageAttributeName,
                                "BO_Report");
                            navigationItem.Items.Add(DocumentsNavigationGroup);
                        }
                    }
                }
            }
        }

        void showNavigationItemController_CustomShowNavigationItem(
            object sender, CustomShowNavigationItemEventArgs e) {
            if((e.ActionArguments.SelectedChoiceActionItem != null) &&
                e.ActionArguments.SelectedChoiceActionItem.Id == "CustomDocuments") {
                ChoiceActionItem parent =
                    e.ActionArguments.SelectedChoiceActionItem.ParentItem;
                ObjectSpace objectSpace = Application.CreateObjectSpace();
                CollectionSource collectionSource = new CollectionSource(
                    objectSpace, typeof(HelpDocument));
                DictionaryNode parentViewInfo =
                    Application.GetViewInfo(parent.Info.GetAttribute("ViewID").Value);
                collectionSource.Criteria.Add("CustomDocumentsController",
                    CriteriaOperator.Parse("ObjectType == '" +
                    parentViewInfo.GetAttribute("ClassName").Value +
                    "'"));
                View view = Application.CreateListView(
                    "HelpDocument_ListView_FewColumns", collectionSource, true);
                e.ActionArguments.ShowViewParameters.CreatedView = view;
                parentViewClassName = parentViewInfo.GetAttribute("ClassName").Value;
                view.CustomizeViewShortcut += new
                EventHandler<CustomizeViewShortcutArgs>(view_CustomizeViewShortcut);
                e.Handled = true;
            }
        }

        void view_CustomizeViewShortcut(object sender, CustomizeViewShortcutArgs e) {
            if(!string.IsNullOrEmpty(parentViewClassName)) {
                e.ViewShortcut[CriteriaKeyName] = parentViewClassName;
            }
        }

        private void UnsubscribeFromEvents() {
            if(showNavigationItemController != null) {
                showNavigationItemController.CustomShowNavigationItem -=
                    new EventHandler<CustomShowNavigationItemEventArgs>(
                    showNavigationItemController_CustomShowNavigationItem);
                showNavigationItemController.NavigationItemCreated -=
                    new EventHandler<NavigationItemCreatedEventArgs>(
                    showNavigationItemController_NavigationItemCreated);
                showNavigationItemController = null;
            }
        }

        protected override void Dispose(bool disposing) {
            UnsubscribeFromEvents();
            base.Dispose(disposing);
        }
    }
}

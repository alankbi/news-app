// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace NewsApp
{
    [Register ("FirstViewController")]
    partial class FirstViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton testButton { get; set; }

        [Action ("DoTestClustering:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DoTestClustering (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (testButton != null) {
                testButton.Dispose ();
                testButton = null;
            }
        }
    }
}
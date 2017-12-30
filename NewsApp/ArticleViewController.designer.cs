// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace NewsApp
{
    [Register ("ArticleViewController")]
    partial class ArticleViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView articleTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (articleTitle != null) {
                articleTitle.Dispose ();
                articleTitle = null;
            }
        }
    }
}
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUserMode.Activities
{
    /// <summary>
    /// A <see cref="Activity"/> which delays execution until a bookmark is resumed
    /// </summary>
    public class WaitForBookmarkActivity : NativeActivity
    {
        /// <summary>
        /// Gets and sets the name of the bookmark
        /// </summary>
        public InArgument<string> BookmarkName { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Activity"/> can introduce an idle state
        /// </summary>
        protected override bool CanInduceIdle { get { return true; } }

        /// <summary>
        /// ?Creates a bookmark and forces an idle state
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(NativeActivityContext context)
        {
            var bookmarkName = this.BookmarkName.Get(context);

            context.CreateBookmark(
                bookmarkName,
                (activityContext, bookmark, value) =>
                    activityContext.ResumeBookmark(new Bookmark(this.BookmarkName.Get(activityContext)), null));
        }
    }
}

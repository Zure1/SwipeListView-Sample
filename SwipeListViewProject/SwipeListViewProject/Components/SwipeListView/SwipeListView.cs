using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace SwipeListViewProject.Components.SwipeListView
{
    public class SwipeListView : ListView
    {
        private List<SwipeItemView> TouchedElements { get; set; } = new List<SwipeItemView>();

        public void AppendTouchedElement(SwipeItemView item)
        {
            TouchedElements.Add(item);
        }
    }
}

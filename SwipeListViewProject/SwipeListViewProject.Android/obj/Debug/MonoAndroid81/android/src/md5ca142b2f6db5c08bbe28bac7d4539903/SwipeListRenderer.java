package md5ca142b2f6db5c08bbe28bac7d4539903;


public class SwipeListRenderer
	extends md51558244f76c53b6aeda52c8a337f2c37.ListViewRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_dispatchTouchEvent:(Landroid/view/MotionEvent;)Z:GetDispatchTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("SwipeListViewProject.Droid.CustomRenderers.SwipeListRenderer, SwipeListViewProject.Android", SwipeListRenderer.class, __md_methods);
	}


	public SwipeListRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == SwipeListRenderer.class)
			mono.android.TypeManager.Activate ("SwipeListViewProject.Droid.CustomRenderers.SwipeListRenderer, SwipeListViewProject.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public SwipeListRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == SwipeListRenderer.class)
			mono.android.TypeManager.Activate ("SwipeListViewProject.Droid.CustomRenderers.SwipeListRenderer, SwipeListViewProject.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public SwipeListRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == SwipeListRenderer.class)
			mono.android.TypeManager.Activate ("SwipeListViewProject.Droid.CustomRenderers.SwipeListRenderer, SwipeListViewProject.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public boolean dispatchTouchEvent (android.view.MotionEvent p0)
	{
		return n_dispatchTouchEvent (p0);
	}

	private native boolean n_dispatchTouchEvent (android.view.MotionEvent p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

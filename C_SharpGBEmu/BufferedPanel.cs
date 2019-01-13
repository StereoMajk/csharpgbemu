// MyControlLibrary.BufferedPanel
using System.ComponentModel;
using System.Windows.Forms;

public class BufferedPanel : UserControl
{
	private IContainer components = null;

	public BufferedPanel()
	{
		SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
		InitializeComponent();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		SuspendLayout();
		base.Name = "BufferedPanel";
		ResumeLayout(performLayout: false);
	}
}


namespace System
{
    internal class DoWorkEventArgs
    {
        private Action<object, ComponentModel.DoWorkEventArgs> sendbtn_Click;

        public DoWorkEventArgs(Action<object, ComponentModel.DoWorkEventArgs> sendbtn_Click)
        {
            this.sendbtn_Click = sendbtn_Click;
        }
    }
}
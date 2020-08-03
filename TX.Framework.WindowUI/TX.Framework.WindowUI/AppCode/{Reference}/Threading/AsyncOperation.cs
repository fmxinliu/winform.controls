#region COPYRIGHT
//
//     THIS IS GENERATED BY TEMPLATE
//     
//     AUTHOR  :     ROYE
//     DATE       :     2011
//
//     COPYRIGHT (C) 2011, TIANXIAHOTEL TECHNOLOGIES CO., LTD. ALL RIGHTS RESERVED.
//
#endregion

using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace System.Threading {
    public abstract class AsyncOperation {
        private ISynchronizeInvoke target;
        private bool isRunning;
        private bool cancelledFlag;
        private bool completeFlag;
        private bool cancelAcknowledgedFlag;
        private bool failedFlag;

        public event EventHandler Completed;
        public event EventHandler Cancelled;
        public event ThreadExceptionEventHandler Failed;

        public AsyncOperation(ISynchronizeInvoke target) {
            this.target = target;
            this.isRunning = false;
        }

        protected abstract void DoWork();

        public void Start() {
            lock (this) {
                if (isRunning) {
                    throw new AlreadyRunningException();
                }
                isRunning = true;
            }
            new MethodInvoker(InternalStart).BeginInvoke(null, null);
        }

        private void InternalStart() {
            cancelledFlag = false;
            completeFlag = false;
            cancelAcknowledgedFlag = false;
            failedFlag = false;
            try {
                DoWork();
            }
            catch (Exception e) {
                try {
                    FailOperation(e);
                }
                catch { }

                if (e is SystemException) {
                    throw;
                }
            }

            lock (this) {
                if (!cancelAcknowledgedFlag && !failedFlag) {
                    CompleteOperation();
                }
            }
        }

        private void CompleteOperation() {
            lock (this) {
                completeFlag = true;
                isRunning = false;
                Monitor.Pulse(this);
                FireAsync(Completed, this, EventArgs.Empty);
            }
        }

        private void FailOperation(Exception e) {
            lock (this) {
                failedFlag = true;
                isRunning = false;
                Monitor.Pulse(this);
                FireAsync(Failed, this, new ThreadExceptionEventArgs(e));
            }
        }

        protected void FireAsync(Delegate dlg, params object[] pList) {
            if (dlg != null) {
                Target.BeginInvoke(dlg, pList);
            }
        }

        public void Cancel() {
            lock (this) {
                cancelledFlag = true;
            }
        }

        public bool CancelAndWait() {
            lock (this) {
                cancelledFlag = true;
                while (!IsDone) {
                    Monitor.Wait(this, 1000);
                }
            }
            return !HasCompleted;
        }

        public bool WaitUntilDone() {
            lock (this) {
                while (!IsDone) {
                    Monitor.Wait(this, 1000);
                }
            }
            return HasCompleted;
        }

        protected void AcknowledgeCancel() {
            lock (this) {
                cancelAcknowledgedFlag = true;
                isRunning = false;

                Monitor.Pulse(this);

                FireAsync(Cancelled, this, EventArgs.Empty);
            }
        }

        public bool IsDone {
            get {
                lock (this) {
                    return completeFlag || cancelAcknowledgedFlag || failedFlag;
                }
            }
        }

        protected ISynchronizeInvoke Target {
            get { return target; }
        }

        protected bool CancelRequested {
            get {
                lock (this) {
                    return cancelledFlag;
                }
            }
        }

        protected bool HasCompleted {
            get {
                lock (this) {
                    return completeFlag;
                }
            }
        }
    }

    public class AlreadyRunningException : ApplicationException {
        public AlreadyRunningException() : base("异步操作已经运行") { }
    }
}

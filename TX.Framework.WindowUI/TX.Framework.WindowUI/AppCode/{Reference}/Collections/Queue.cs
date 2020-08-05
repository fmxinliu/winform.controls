#region COPYRIGHT
//
//     THIS IS GENERATED BY TEMPLATE
//     
//     AUTHOR  :     ROYE
//     DATE       :     2010
//
//     COPYRIGHT (C) 2010, TIANXIAHOTEL TECHNOLOGIES CO., LTD. ALL RIGHTS RESERVED.
//
#endregion

namespace System.Collections {
    /// <summary>
    /// <para>
    /// A data structure commonly used for routing and buffering work (or any objects) between different
    /// threads in the same process; a queue may also be used by a single thread as a way
    /// to organize pieces of work in a simple way.
    /// </para>
    /// <para>
    /// Two subclasses, DoubleEndedQueue and PriorityQueue, are provided (see their documentation
    /// for more details).  Since they share a common parent class, code receiving objects
    /// from a Queue does not need to know the actual implementation, except in the special
    /// case where an object must be removed from the front of a double-ended queue.  Either
    /// DoubleEndedQueue or PriorityQueue may be used to good effect as a simple single-ended queue
    /// by using the methods overridden from this class, Queue.
    /// </para>
    /// <para>
    /// The queue classes are provided in order to remedy the following failings of
    /// System.Collections.Queue: <br>
    /// - lack of support for double-ended and priority queues, two commonly-needed types
    /// in programming many different types of systems <br>
    /// - lack of support for waiting with timeout in case of nothing being immediately
    /// available in the queue; this leads to polling, with unavoidable loss averaging
    /// half the polling interval <br>
    /// - slow performance when synchronized <br>
    /// - poor naming (method names Add and Remove are more easily and intuitively understood than 
    /// Enqueue and Dequeue, leading to more readable code)<br>
    /// </para>
    /// <para>
    /// All System.Collections.Queue instances are synchronized.  This is in recognition
    /// of the main use of such in-memory structures in actual practice, which is to route
    /// objects between threads in the same process.  Multiple threads requires synchronization,
    /// and the approach in the System.Collections namespace (wrapping an unsynchronized object in 
    /// a synchronized wrapper) unnecessarily slows performance in this situation due to 
    /// unnecessary method calls.
    /// </para>
    /// </summary>
    public abstract class Queue {
        private static object[] EMPTY_OBJECT_ARRAY = { };

        protected bool isOpen = true;

        protected int count = 0;

        protected bool isNullAllowed = false;

        protected object monitor = new object();

        /// <summary>
        /// Adds an element to the queue.  Implemented in subclasses
        /// </summary>
        /// <param name="_object">The element to add</param>
        public abstract void Add(object _object);

        /// <summary>
        /// Gets the next object from the queue without removing it.  Implemented in subclasses
        /// </summary>
        public abstract object Peek();

        /// <summary>
        /// Gets the next object from the queue without removing it.  If no object is available,
        /// waits up to the specified number of milliseconds, then returns null if no object is
        /// available.
        /// Implemented in subclasses
        /// </summary>
        public abstract object Peek(int _millisecondsTimeout);

        /// <summary>
        /// Removes the next object from the queue.  Implemented in subclasses
        /// </summary>
        public abstract object Remove();

        /// <summary>
        /// Removes the next object from the queue.  If no object is available,
        /// waits up to the specified number of milliseconds, then returns null if no object is
        /// available.
        /// Implemented in subclasses
        /// </summary>
        public abstract object Remove(int _millisecondsTimeout);

        /// <summary>
        /// Removes all objects from the queue and returns them in ascending order, with 
        /// the element at index zero being the one "next in line"
        /// </summary>
        public virtual object[] RemoveAll() {
            lock (monitor) {
                if (count == 0) {
                    return EMPTY_OBJECT_ARRAY;
                }
                else {
                    object[] elements = new object[count];
                    for (int x = 0; x < elements.Length; x++) {
                        elements[x] = Remove();
                    }
                    return elements;
                }
            }
        }

        /// <summary>
        /// Releases waiting threads, immediately returning null values to them, but
        /// leaves the queue open
        /// </summary>
        public void ReleaseWaitingThreads() {
            lock (monitor) {
                bool open = isOpen;
                Close();
                isOpen = open;
            }
        }

        /// <summary>
        /// Allows the queue to accept input.  Each Queue instance is open when
        /// it is created.
        /// </summary>
        public virtual void Open() {
            lock (monitor) {
                isOpen = true;
            }
        }

        /// <summary>
        /// Renders the queue incapable of accepting further input; also releases all
        /// waiting threads, immediately returning them null values
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Indicates whether the queue can accept further input
        /// </summary>
        public bool IsOpen {
            get {
                lock (monitor) {
                    return isOpen;
                }
            }
        }

        /// <summary>
        /// Indicates the number of elements currently held in the queue
        /// </summary>
        public int Count {
            get {
                lock (monitor) {
                    return count;
                }
            }
        }

        /// <summary>
        /// Clears all elements from the queue
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Gets and sets whether null values can be added to the queue
        /// </summary>
        public bool IsNullAllowed {
            get {
                lock (monitor) {
                    return isNullAllowed;
                }
            }
            set {
                lock (monitor) {
                    isNullAllowed = value;
                }
            }
        }
    }
}
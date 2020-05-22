using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Flows
{
    internal class Roamable<T> : IEnumerator<T>
        where T : ICode
    {
        private readonly T root;
        private ICode next;

        public Roamable(T root)
        {
            
            this.root = root;
            this.next = root.Previous;
        }

        public T Current
        {
            get
            {
                return (T)this.next;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Dispose()
        {
            Reset();
        }

        public bool MoveNext()
        {
            this.next = (T)this.next.Next;
            return (this.next is T);
        }

        public void Reset()
        {
            next = root;
        }
    }
}

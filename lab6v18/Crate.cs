using System;

namespace Lab6v18
{
    public class Crate : Container
    {
        private bool _isStackable;

        public bool IsStackable
        {
            get { return _isStackable; }
            set { _isStackable = value; }
        }

        public Crate(int capacity, string material, bool isStackable)
            : base(capacity, material)
        {
            IsStackable = isStackable;
        }

        public override void StoreItem(string item)
        {
            Console.WriteLine("[Ящик] Вантажимо в ящик: " + item);
            base.StoreItem(item);
        }

        public void StackCrate()
        {
            if (IsStackable)
            {
                Console.WriteLine("[Ящик] Ящик поставлено на iнший ящик");
            }
            else
            {
                Console.WriteLine("[Ящик] Цей ящик не можна штабелювати");
            }
        }
    }
}
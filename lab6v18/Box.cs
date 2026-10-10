using System;

namespace Lab6v18
{
    public class Box : Container
    {
        private bool _isSealed;

        public bool IsSealed
        {
            get { return _isSealed; }
            set { _isSealed = value; }
        }

        public Box(int capacity, string material, bool isSealed)
            : base(capacity, material)
        {
            IsSealed = isSealed;
        }

        public override void StoreItem(string item)
        {
            if (IsSealed)
            {
                Console.WriteLine("[Коробка] Коробка запечатана, не можна покласти: " + item);
                return;
            }
            Console.WriteLine("[Коробка] Кладемо в коробку: " + item);
            base.StoreItem(item);
        }

        public void CloseBox()
        {
            IsSealed = true;
            Console.WriteLine("[Коробка] Коробку запечатано");
        }

        public new string GetContainerType()
        {
            return "Коробка";
        }
    }
}
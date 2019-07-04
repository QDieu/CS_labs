using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace lab4 {
    class BitContainer : IEnumerable<bool> {
        public List<byte> list;
        public int Length { get; private set; }

        public BitContainer() {
            list = new List<byte>();
            Length = 0;
        }

        public void PushBit(int bit) {
            if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException("Incorrect value");
            if (Length % 8 == 0) list.Add(0); // push fictive falue to list
            Length++;
            SetBit(bit, Length);
        }

        public void PushBit(bool bit) {
            int tempBit = (bit) ? 1 : 0;
            PushBit(tempBit);
        }

        private void SetBit(int position, int bit) {
            //if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException("Incorrect value");
            if (position < 0 || position > Length) throw new IndexOutOfRangeException("Wrong index");
            int bytePlace = position / 8;
            int offset = position % 8;
            int currentByte = list[bytePlace];

            currentByte &= ~(1 << offset);
            list[bytePlace] = (byte)(currentByte | (bit << offset));

        }

        private void SetBit(int position, bool bit) {
            int tempBit = (bit) ? 1 : 0;
            SetBit(position, tempBit);
        }

        private int GetBit(int position) {
            if (position < 0 || position > Length) throw new IndexOutOfRangeException("Wrong index");
            int bytePlace = position / 8;
            int offset = position % 8;

            int bit = (list[bytePlace] & (1 << offset)) > 0 ? 1 : 0;
            return bit;
        }

        public void Clear() {
            Length = 0;
            list.Clear();
        }

        public int this[int index] {
            get => GetBit(index);
            set => SetBit(value, index);
        }

        public void Insert(int position, int bit) {
            if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException("Incorrect value");
            if (position < 0 || position > Length) throw new IndexOutOfRangeException("Wrong index");
            PushBit(this[Length - 1]);
            for (int i = Length - 1; i > position; --i) this[i] = this[i - 1];
            this[position] = bit;
        }

        public void Insert(int position, bool bit) {
            int tempBit = (bit) ? 1 : 0;
            Insert(position, tempBit);
        }

        public void Remove(int position) {
            if (position < 0 || position > Length) throw new IndexOutOfRangeException("Wrong index");
            for (int i = position; i < Length - 1; ++i)
                this[i] = this[i + 1];
            Length--;
        }

        public IEnumerator<bool> GetEnumerator() {
            for (int i = 0; i < Length; ++i)
                yield return Convert.ToBoolean(this[i]);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public override string ToString() {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < list.Count; ++i) {
                str.Append(Convert.ToString(list[i], toBase: 2));
                str.Append(" ");
            }
            str.Append("\n");
            return str.ToString();
        }
    }
}

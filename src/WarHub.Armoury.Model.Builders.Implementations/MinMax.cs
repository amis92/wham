﻿namespace WarHub.Armoury.Model.Builders.Implementations
{
    using Mvvm;

    internal class MinMax<T> : NotifyPropertyChangedBase, IMinMax<T>
    {
        private T _max;
        private T _min;

        public T Max
        {
            get { return _max; }
            set { Set(ref _max, value); }
        }

        public T Min
        {
            get { return _min; }
            set { Set(ref _min, value); }
        }
    }
}
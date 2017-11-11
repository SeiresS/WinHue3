﻿using WinHue3.Utils;

namespace WinHue3.Functions.Application_Settings
{
    public class AppDefaultsModel : ValidatableBindableBase
    {
        private ushort? _allOnTT;
        private ushort? _allOffTT;
        private ushort? _defaultTT;
        private byte _defaultLightBri;
        private byte _defaultGroupBri;

        public AppDefaultsModel()
        {
            _allOffTT = null;
            _allOnTT = null;
            _defaultTT = null;
            _defaultLightBri = 255;
            _defaultGroupBri = 255;
        }

        public ushort? AllOnTt
        {
            get => _allOnTT;
            set => SetProperty(ref _allOnTT,value);
        }

        public ushort? AllOffTt
        {
            get => _allOffTT;
            set => SetProperty(ref _allOffTT,value);
        }

        public ushort? DefaultTt
        {
            get => _defaultTT;
            set => SetProperty(ref _defaultTT,value);
        }

        public byte DefaultLightBri
        {
            get => _defaultLightBri;
            set => SetProperty(ref _defaultLightBri,value);
        }

        public byte DefaultGroupBri
        {
            get => _defaultGroupBri;
            set => SetProperty(ref _defaultGroupBri,value);
        }
    }
}

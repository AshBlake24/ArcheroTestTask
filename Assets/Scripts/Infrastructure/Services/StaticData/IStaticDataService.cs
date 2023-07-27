﻿using System;
using System.Collections.Generic;

namespace Source.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();

        TResult GetDataById<TKey, TResult>(TKey id) where TKey : Enum 
            where TResult : IStaticData;

        IEnumerable<TData> GetAllDataByType<TEnum, TData>() 
            where TEnum : Enum 
            where TData : IStaticData;
    }
}
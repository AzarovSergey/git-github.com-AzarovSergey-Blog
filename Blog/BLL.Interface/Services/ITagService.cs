﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface ITagService
    {
        void Add(TagEntity tag);
        IEnumerable<TagEntity> GetAll();
        IEnumerable<TagEntity> GetByArticleId(int articleId);
    }
}

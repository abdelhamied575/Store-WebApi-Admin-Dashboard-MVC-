using Microsoft.EntityFrameworkCore;
using StoreWeb.Core.Entities;
using StoreWeb.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Repository.Specifications_Evaluator
{
    public static class SpecificationsEvaluator<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity,TKey> spec)
        {
            var query = inputQuery;

            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if(spec.OrderByDescending  is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }


            if (spec.IsPaginationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }


            query= spec.Includes.Aggregate(query, (currentQuery, includeExperssion) => currentQuery.Include(includeExperssion));


            return query;


        }


    }
}

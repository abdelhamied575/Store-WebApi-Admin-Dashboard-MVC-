﻿using StoreWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Specifications.Products
{
    public class ProductSpecifications:BaseSpecifications<Product ,int>
    {
        public ProductSpecifications(int id):base(P=>P.Id==id)
        {
            ApplyIncludes();
        }

        public ProductSpecifications(ProductSpecParams productSpec) :base(
            
            P=>
            (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search) )
            &&
            (!productSpec.BrandId.HasValue || productSpec.BrandId==P.BrandId)
            && (!productSpec.TypeId.HasValue || productSpec.TypeId == P.TypeId)
            )
        {



            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                productSpec.Sort = productSpec.Sort.ToLower(); 
                switch (productSpec.Sort)
                {       
                    case "priceasc":
                        AddOrderBy(P => P.Price); break;
                    case "pricedesc":
                        AddOrderByDescending( P => P.Price); break;
                    default:
                        AddOrderBy( P => P.Name); break;
                }
            }
            else
            {
                AddOrderBy(P => P.Id);
            }

            ApplyIncludes();


            ApplyPagination(productSpec.PageSize*(productSpec.PageIndex-1),productSpec.PageSize);



        }


        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }

    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 31/12/2008 12:23:27
// This source code was auto-generated by NHQG (NHQG), Version 2.0.0.0.
// 
namespace Query {
    using NHibernate = global::NHibernate;
    
    
    public partial class Where {
        
        /// <summary>
        /// Query helper for member Where.PropertyBag
        /// </summary>
        [System.CLSCompliantAttribute(false)]
        public static Root_Query_PropertyBag PropertyBag {
            get {
                return new Root_Query_PropertyBag();
            }
        }
        
        /// <summary>
        /// Query helper for member Where.Query_PropertyBag
        /// </summary>
        [System.CLSCompliantAttribute(false)]
        public partial class Query_PropertyBag<T1> : QueryBuilder<T1>
         {
            
            /// <summary>
            /// Query helper for member Query_PropertyBag..ctor
            /// </summary>
            public Query_PropertyBag() : 
                    this(null, "this", null) {
            }
            
            /// <summary>
            /// Query helper for member Query_PropertyBag..ctor
            /// </summary>
            public Query_PropertyBag(QueryBuilder<T1> parent, string name, string associationPath) : 
                    base(parent, name, associationPath) {
            }
            
            /// <summary>
            /// Query helper for member Query_PropertyBag..ctor
            /// </summary>
            public Query_PropertyBag(QueryBuilder<T1> parent, string name, string associationPath, bool backTrackAssociationOnEquality) : 
                    base(parent, name, associationPath, backTrackAssociationOnEquality) {
            }
            
            /// <summary>
            /// Query helper for member Query_PropertyBag.
            /// </summary>
            public virtual PropertyQueryBuilder<T1> Name {
                get {
                    string temp = associationPath;
                    PropertyQueryBuilder<T1> child = new PropertyQueryBuilder<T1>(null, "Name", temp);
                    child.ShouldSkipJoinOnIdEquality = this.ShouldSkipJoinOnIdEquality;
                    return child;
                }
            }
            
            /// <summary>
            /// Query helper for member Query_PropertyBag.
            /// </summary>
            public virtual IdQueryBuilder<T1> Id {
                get {
                    string temp = associationPath;
                    IdQueryBuilder<T1> child = new IdQueryBuilder<T1>(null, "Id", temp);
                    child.ShouldSkipJoinOnIdEquality = this.ShouldSkipJoinOnIdEquality;
                    return child;
                }
            }
        }
        
        /// <summary>
        /// Query helper for member Where.Root_Query_PropertyBag
        /// </summary>
        [System.CLSCompliantAttribute(false)]
        public partial class Root_Query_PropertyBag : Query_PropertyBag<Membrane.Models.PropertyBag> {
        }
    }
    
    public partial class OrderBy {
        
        /// <summary>
        /// Query helper for member OrderBy.PropertyBag
        /// </summary>
        [System.CLSCompliantAttribute(false)]
        public partial class PropertyBag {
            
            /// <summary>
            /// Query helper for member PropertyBag.Name
            /// </summary>
            public static OrderByClause Name {
                get {
                    return new OrderByClause("Name");
                }
            }
            
            /// <summary>
            /// Query helper for member PropertyBag.Id
            /// </summary>
            public static OrderByClause Id {
                get {
                    return new OrderByClause("Id");
                }
            }
        }
    }
    
    public partial class ProjectBy {
        
        /// <summary>
        /// Query helper for member ProjectBy.PropertyBag
        /// </summary>
        [System.CLSCompliantAttribute(false)]
        public partial class PropertyBag {
            
            /// <summary>
            /// Query helper for member PropertyBag.Name
            /// </summary>
            public static PropertyProjectionBuilder Name {
                get {
                    return new PropertyProjectionBuilder("Name");
                }
            }
            
            /// <summary>
            /// Query helper for member PropertyBag.Id
            /// </summary>
            public static NumericPropertyProjectionBuilder Id {
                get {
                    return new NumericPropertyProjectionBuilder("Id");
                }
            }
        }
    }
    
    public partial class GroupBy {
        
        /// <summary>
        /// Query helper for member GroupBy.PropertyBag
        /// </summary>
        [System.CLSCompliantAttribute(false)]
        public partial class PropertyBag {
            
            /// <summary>
            /// Query helper for member PropertyBag.Name
            /// </summary>
            public static NHibernate.Criterion.IProjection Name {
                get {
                    return NHibernate.Criterion.Projections.GroupProperty("Name");
                }
            }
            
            /// <summary>
            /// Query helper for member PropertyBag.Id
            /// </summary>
            public static NHibernate.Criterion.IProjection Id {
                get {
                    return NHibernate.Criterion.Projections.GroupProperty("Id");
                }
            }
        }
    }
}
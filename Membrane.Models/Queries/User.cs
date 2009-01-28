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
        /// Query helper for member Where.User
        /// </summary>
        [System.CLSCompliantAttribute(false)]
        public static Root_Query_User User {
            get {
                return new Root_Query_User();
            }
        }
        
        /// <summary>
        /// Query helper for member Where.Query_User
        /// </summary>
        [System.CLSCompliantAttribute(false)]
        public partial class Query_User<T1> : QueryBuilder<T1>
         {
            
            /// <summary>
            /// Query helper for member Query_User..ctor
            /// </summary>
            public Query_User() : 
                    this(null, "this", null) {
            }
            
            /// <summary>
            /// Query helper for member Query_User..ctor
            /// </summary>
            public Query_User(QueryBuilder<T1> parent, string name, string associationPath) : 
                    base(parent, name, associationPath) {
            }
            
            /// <summary>
            /// Query helper for member Query_User..ctor
            /// </summary>
            public Query_User(QueryBuilder<T1> parent, string name, string associationPath, bool backTrackAssociationOnEquality) : 
                    base(parent, name, associationPath, backTrackAssociationOnEquality) {
            }
            
            /// <summary>
            /// Query helper for member Query_User.
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
            /// Query helper for member Query_User.
            /// </summary>
            public virtual PropertyQueryBuilder<T1> Username {
                get {
                    string temp = associationPath;
                    PropertyQueryBuilder<T1> child = new PropertyQueryBuilder<T1>(null, "Username", temp);
                    child.ShouldSkipJoinOnIdEquality = this.ShouldSkipJoinOnIdEquality;
                    return child;
                }
            }
            
            /// <summary>
            /// Query helper for member Query_User.
            /// </summary>
            public virtual PropertyQueryBuilder<T1> Email {
                get {
                    string temp = associationPath;
                    PropertyQueryBuilder<T1> child = new PropertyQueryBuilder<T1>(null, "Email", temp);
                    child.ShouldSkipJoinOnIdEquality = this.ShouldSkipJoinOnIdEquality;
                    return child;
                }
            }
            
            /// <summary>
            /// Query helper for member Query_User.
            /// </summary>
            public virtual PropertyQueryBuilder<T1> Password {
                get {
                    string temp = associationPath;
                    PropertyQueryBuilder<T1> child = new PropertyQueryBuilder<T1>(null, "Password", temp);
                    child.ShouldSkipJoinOnIdEquality = this.ShouldSkipJoinOnIdEquality;
                    return child;
                }
            }
            
            /// <summary>
            /// Query helper for member Query_User.
            /// </summary>
            public virtual IdQueryBuilder<T1> Id {
                get {
                    string temp = associationPath;
                    IdQueryBuilder<T1> child = new IdQueryBuilder<T1>(null, "Id", temp);
                    child.ShouldSkipJoinOnIdEquality = this.ShouldSkipJoinOnIdEquality;
                    return child;
                }
            }
            
            /// <summary>
            /// Query helper for member Query_User.
            /// </summary>
            public virtual Query_UserType<T1> Type {
                get {
                    string temp = associationPath;
                    temp = ((temp + ".") 
                                + "Type");
                    Query_UserType<T1> child = new Query_UserType<T1>(null, "Type", temp, true);
                    child.ShouldSkipJoinOnIdEquality = this.ShouldSkipJoinOnIdEquality;
                    return child;
                }
            }
            
            public virtual Query_Collection_Groups Groups {
                get {
                    string temp = associationPath;
                    temp = (temp + ".Groups");
                    return new Query_Collection_Groups(this, "Groups", temp);
                }
            }
            
            [System.CLSCompliantAttribute(false)]
            public class Query_Collection_Groups : CollectionQueryBuilder<T1> {
                
                public Query_Collection_Groups(QueryBuilder<T1> parent, string name, string associationPath) : 
                        base(parent, name, associationPath) {
                }
                
                public virtual Query_Group<T1> With() {
                    Query_Group<T1> query = new Query_Group<T1>(this, this.myName, this.associationPath);
                    query.joinType = NHibernate.SqlCommand.JoinType.InnerJoin;
                    query.fetchMode = NHibernate.FetchMode.Default;
                    query.ShouldSkipJoinOnIdEquality = true;
                    return query;
                }
                
                public virtual Query_Group<T1> With(NHibernate.SqlCommand.JoinType joinType) {
                    Query_Group<T1> query = new Query_Group<T1>(this, this.myName, this.associationPath);
                    query.joinType = joinType;
                    query.fetchMode = NHibernate.FetchMode.Default;
                    query.ShouldSkipJoinOnIdEquality = true;
                    return query;
                }
                
                public virtual Query_Group<T1> With(NHibernate.FetchMode fetchMode) {
                    Query_Group<T1> query = new Query_Group<T1>(this, this.myName, this.associationPath);
                    query.joinType = NHibernate.SqlCommand.JoinType.InnerJoin;
                    query.fetchMode = fetchMode;
                    query.ShouldSkipJoinOnIdEquality = true;
                    return query;
                }
                
                public virtual Query_Group<T1> With(NHibernate.SqlCommand.JoinType joinType, NHibernate.FetchMode fetchMode) {
                    Query_Group<T1> query = new Query_Group<T1>(this, this.myName, this.associationPath);
                    query.joinType = joinType;
                    query.fetchMode = fetchMode;
                    query.ShouldSkipJoinOnIdEquality = true;
                    return query;
                }
            }
        }
        
        /// <summary>
        /// Query helper for member Where.Root_Query_User
        /// </summary>
        [System.CLSCompliantAttribute(false)]
        public partial class Root_Query_User : Query_User<Membrane.Models.UserManagement.User> {
        }
    }
    
    public partial class OrderBy {
        
        /// <summary>
        /// Query helper for member OrderBy.User
        /// </summary>
        [System.CLSCompliantAttribute(false)]
        public partial class User {
            
            /// <summary>
            /// Query helper for member User.Name
            /// </summary>
            public static OrderByClause Name {
                get {
                    return new OrderByClause("Name");
                }
            }
            
            /// <summary>
            /// Query helper for member User.Username
            /// </summary>
            public static OrderByClause Username {
                get {
                    return new OrderByClause("Username");
                }
            }
            
            /// <summary>
            /// Query helper for member User.Email
            /// </summary>
            public static OrderByClause Email {
                get {
                    return new OrderByClause("Email");
                }
            }
            
            /// <summary>
            /// Query helper for member User.Password
            /// </summary>
            public static OrderByClause Password {
                get {
                    return new OrderByClause("Password");
                }
            }
            
            /// <summary>
            /// Query helper for member User.Id
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
        /// Query helper for member ProjectBy.User
        /// </summary>
        [System.CLSCompliantAttribute(false)]
        public partial class User {
            
            /// <summary>
            /// Query helper for member User.Name
            /// </summary>
            public static PropertyProjectionBuilder Name {
                get {
                    return new PropertyProjectionBuilder("Name");
                }
            }
            
            /// <summary>
            /// Query helper for member User.Username
            /// </summary>
            public static PropertyProjectionBuilder Username {
                get {
                    return new PropertyProjectionBuilder("Username");
                }
            }
            
            /// <summary>
            /// Query helper for member User.Email
            /// </summary>
            public static PropertyProjectionBuilder Email {
                get {
                    return new PropertyProjectionBuilder("Email");
                }
            }
            
            /// <summary>
            /// Query helper for member User.Password
            /// </summary>
            public static PropertyProjectionBuilder Password {
                get {
                    return new PropertyProjectionBuilder("Password");
                }
            }
            
            /// <summary>
            /// Query helper for member User.Id
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
        /// Query helper for member GroupBy.User
        /// </summary>
        [System.CLSCompliantAttribute(false)]
        public partial class User {
            
            /// <summary>
            /// Query helper for member User.Name
            /// </summary>
            public static NHibernate.Criterion.IProjection Name {
                get {
                    return NHibernate.Criterion.Projections.GroupProperty("Name");
                }
            }
            
            /// <summary>
            /// Query helper for member User.Username
            /// </summary>
            public static NHibernate.Criterion.IProjection Username {
                get {
                    return NHibernate.Criterion.Projections.GroupProperty("Username");
                }
            }
            
            /// <summary>
            /// Query helper for member User.Email
            /// </summary>
            public static NHibernate.Criterion.IProjection Email {
                get {
                    return NHibernate.Criterion.Projections.GroupProperty("Email");
                }
            }
            
            /// <summary>
            /// Query helper for member User.Password
            /// </summary>
            public static NHibernate.Criterion.IProjection Password {
                get {
                    return NHibernate.Criterion.Projections.GroupProperty("Password");
                }
            }
            
            /// <summary>
            /// Query helper for member User.Id
            /// </summary>
            public static NHibernate.Criterion.IProjection Id {
                get {
                    return NHibernate.Criterion.Projections.GroupProperty("Id");
                }
            }
        }
    }
}

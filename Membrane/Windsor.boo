import System.Reflection
import Castle.MonoRail.Framework
import Castle.MonoRail.WindsorExtension
import Rhino.Commons.ForTesting from Rhino.Commons.NHibernate
import Rhino.Commons.ForTesting from Rhino.Commons.ActiveRecord

Facility("monorail", MonoRailFacility)

webAsm = Assembly.Load("Membrane")
activeRecordAsm = (Assembly.Load("Membrane.Core"), )

for type in webAsm.GetTypes():
	if typeof(Controller).IsAssignableFrom(type):
		Component(type.Name, type)
	elif typeof(ViewComponent).IsAssignableFrom(type):
		Component(type.Name, type)
		
		
Component("active_record_repository", IRepository, ARRepository)
Component("active_record_unit_of_work", 
	IUnitOfWorkFactory,
	ActiveRecordUnitOfWorkFactory,
	assemblies: activeRecordAsm)
@echo off

call dllcopy.bat		../../PhysX_3.4/bin/vc11win32 ../bin/vc11win32-PhysX_3.4 ../../PxShared/bin/vc11win32 ../../PhysX_3.4/bin/vc11win32 
call dll64copy.bat	../../PhysX_3.4/bin/vc11win64 ../bin/vc11win64-PhysX_3.4 ../../PxShared/bin/vc11win64 ../../PhysX_3.4/bin/vc11win64 
call dllcopy.bat		../../PhysX_3.4/bin/vc12win32 ../bin/vc12win32-PhysX_3.4 ../../PxShared/bin/vc12win32 ../../PhysX_3.4/bin/vc12win32 
call dll64copy.bat	../../PhysX_3.4/bin/vc12win64 ../bin/vc12win64-PhysX_3.4 ../../PxShared/bin/vc12win64 ../../PhysX_3.4/bin/vc12win64 
call dllcopy.bat		../../PhysX_3.4/bin/vc14win32 ../bin/vc14win32-PhysX_3.4 ../../PxShared/bin/vc14win32 ../../PhysX_3.4/bin/vc14win32 
call dll64copy.bat	../../PhysX_3.4/bin/vc14win64 ../bin/vc14win64-PhysX_3.4 ../../PxShared/bin/vc14win64 ../../PhysX_3.4/bin/vc14win64 
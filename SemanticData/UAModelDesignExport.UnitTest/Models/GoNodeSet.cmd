set COMPILER=mdc

del %1\mdc.log  /q
del %1\mdc.warnings.log /q
"%1\%COMPILER%" compile --d2 "ReferenceTest.xml" -c "ReferenceTest.csv" --o2 "ReferenceTest"
xcopy %1\mdc.log .\ReferenceTest\mdc.log /y /i
xcopy %1\mdc.warnings.log .\ReferenceTest\mdc.warnings.log /y /i

del %1\mdc.log  /q
del %1\mdc.warnings.log /q
"%1\%COMPILER%" compile --d2 "ObjectTypeTest.xml" -c "ObjectTypeTest.csv" --o2 "ObjectTypeTest"
xcopy %1\mdc.log .\ObjectTypeTest\mdc.log /y /i
xcopy %1\mdc.warnings.log .\ObjectTypeTest\mdc.warnings.log /y /i

del %1\mdc.log  /q
del %1\mdc.warnings.log /q
"%1\%COMPILER%" compile --d2 "VariableTypeTest.xml" -c "VariableTypeTest.csv" --o2 "VariableTypeTest"
xcopy %1\mdc.log .\VariableTypeTest\mdc.log /y /i
xcopy %1\mdc.warnings.log .\VariableTypeTest\mdc.warnings.log /y /i

del %1\mdc.log  /q
del %1\mdc.warnings.log /q
"%1\%COMPILER%" compile --d2 "DataTypeTest.xml" -c "DataTypeTest.csv" --o2 "DataTypeTest"
xcopy %1\mdc.log .\DataTypeTest\mdc.log /y /i
xcopy %1\mdc.warnings.log .\DataTypeTest\mdc.warnings.log /y /i


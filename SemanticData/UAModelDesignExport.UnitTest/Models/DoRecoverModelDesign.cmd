:: Recovering ModelDesign from UANodeSet OPC UA Model Information
:: %1 - location of the asp.exe 

del %1\asp.log  /q
del %1\asp.warnings.log /q
%1\asp "DataTypeTest\DataTypeTest.NodeSet2.xml" -e "DataTypeTest.asp.xml" -s XMLstylesheet -n "http://cas.eu/UA/CommServer/UnitTests/DataTypeTest"
xcopy %1\asp.log DataTypeTest\asp.log /y
xcopy %1\asp.warnings.log DataTypeTest\asp.warnings.log /y /i

del %1\asp.log  /q
del %1\asp.warnings.log /q
%1\asp "ObjectTypeTest\ObjectTypeTest.NodeSet2.xml" -e "ObjectTypeTest.asp.xml" -s XMLstylesheet -n "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"
xcopy %1\asp.log ObjectTypeTest\asp.log /y
xcopy %1\asp.warnings.log ObjectTypeTest\asp.warnings.log /y /i

del %1\asp.log  /q
del %1\asp.warnings.log /q
%1\asp "ReferenceTest\ReferenceTest.NodeSet2.xml" -e "ReferenceTest.asp.xml" -s XMLstylesheet -n "http://cas.eu/UA/CommServer/UnitTests/ReferenceTest"
xcopy %1\asp.log ReferenceTest\asp.log/y
xcopy %1\asp.warnings.log ReferenceTest\asp.warnings.log /y /i

del %1\asp.log  /q
del %1\asp.warnings.log /q
%1\asp "VariableTypeTest\VariableTypeTest.NodeSet2.xml" -e "VariableTypeTest.asp.xml" -s XMLstylesheet -n "http://cas.eu/UA/CommServer/UnitTests/VariableTypeTest"
xcopy %1\asp.log VariableTypeTest\asp.log /y
xcopy %1\asp.warnings.log VariableTypeTest\asp.warnings.log /y /i

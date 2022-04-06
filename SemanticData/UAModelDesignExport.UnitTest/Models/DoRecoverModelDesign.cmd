:: Recovering ModelDesign from UANodeSet OPC UA Model Information
:: %1 - location of the asp.exe 

del %1\asp.log  /q
del %1\asp.warnings.log /q
%1\asp "XMLModels\DataTypeTest.NodeSet2.xml" -e "XMLModels\DataTypeTest.asp.xml" -s XMLstylesheet -n "http://cas.eu/UA/CommServer/UnitTests/DataTypeTest"
xcopy %1\asp.log ReferenceTest.asp.log /y
xcopy %1\asp.warnings.log ReferenceTest.asp.warnings.log /y /i

del %1\asp.log  /q
del %1\asp.warnings.log /q
%1\asp "XMLModels\ObjectTypeTest.NodeSet2.xml" -e "XMLModels\ObjectTypeTest.asp.xml" -s XMLstylesheet -n "http://cas.eu/UA/CommServer/UnitTests/ObjectTypeTest"
xcopy %1\asp.log ReferenceTest.asp.log /y
xcopy %1\asp.warnings.log ReferenceTest.asp.warnings.log /y /i

del %1\asp.log  /q
del %1\asp.warnings.log /q
%1\asp "XMLModels\ReferenceTest.NodeSet2.xml" -e "XMLModels\ReferenceTest.asp.xml" -s XMLstylesheet -n "http://cas.eu/UA/CommServer/UnitTests/ReferenceTest"
xcopy %1\ReferenceTest.asp.log asp.log /y
xcopy %1\asp.warnings.log .\ReferenceTest.asp.warnings.log /y /i

del %1\asp.log  /q
del %1\asp.warnings.log /q
%1\asp "XMLModels\VariableTypeTest.NodeSet2.xml" -e "XMLModels\VariableTypeTest.asp.xml" -s XMLstylesheet -n "http://cas.eu/UA/CommServer/UnitTests/VariableTypeTest"
xcopy %1\asp.log VariableTypeTest.asp.log /y
xcopy %1\asp.warnings.log VariableTypeTest.asp.warnings.log /y /i

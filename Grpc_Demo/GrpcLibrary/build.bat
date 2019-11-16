@echo off
for %%i in (protos/*.proto) do (
 protoc -I . --csharp_out=./OutCsharp  --grpc_out=./OutCsharpGrpc --plugin=protoc-gen-grpc=grpc_csharp_plugin.exe ./protos/%%i
 echo exchange %%i To c# file successfully!
)
pause
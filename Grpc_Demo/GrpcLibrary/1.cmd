protoc -I . --csharp_out . --grpc_out . --plugin=protoc-gen-grpc=grpc_csharp_plugin.exe $SRC_DIR/*.proto  
pause
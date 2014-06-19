@echo off

start "Generating Thrift Code" utils\thrift-0.9.1.exe -out src\NProg.Distributed.Thrift\generated --gen csharp src\NProg.Distributed.Thrift\message.thrift

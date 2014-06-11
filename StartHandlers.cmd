@echo off

start "Add Order Handler" src\NProg.Distributed.Server\bin\debug\NProg.Distributed.Server.exe add-order

start "Get Order Handler" src\NProg.Distributed.Server\bin\debug\NProg.Distributed.Server.exe get-order

start "Remove Order Handler" src\NProg.Distributed.Server\bin\debug\NProg.Distributed.Server.exe remove-order

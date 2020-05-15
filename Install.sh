#!/bin/bash

systemctl stop Sergen.service
rm /lib/systemd/system/Sergen.service
cp Sergen.service /lib/systemd/system/Sergen.service

mkdir -p /opt/Sergen
dotnet build Sergen.sln -c Release

yes | cp -rf src/Sergen.Master/bin/Release/netcoreapp3.1/* /opt/Sergen

systemctl daemon-reload
systemctl start Sergen.service

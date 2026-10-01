#!/bin/sh
set -e

dotnet "bin/${CONFIGURATION_NAME}/net10.0/Monero.Lws.IntegrationTests.dll" \
  --output Detailed \
  --coverlet \
  --coverlet-output-format cobertura

mkdir -p /TestResults/coverage/integration

mv "bin/${CONFIGURATION_NAME}/net10.0/TestResults"/coverage.cobertura.*.xml \
   /TestResults/coverage/integration/coverage.cobertura.xml

reportgenerator \
  -reports:"/TestResults/coverage/integration/coverage.cobertura.xml" \
  -targetdir:"/TestResults/coverage" \
  -reporttypes:"HtmlSummary;Cobertura"
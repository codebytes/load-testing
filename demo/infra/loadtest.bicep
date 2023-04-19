param loadTestName string
param location string = 'eastus'

resource loadtest 'Microsoft.LoadTestService/loadTests@2022-12-01' = {
  name: loadTestName
  location: location
  identity: {
    type: 'SystemAssigned '
  }
  properties: {
    description: 'demo load test'
  }
}

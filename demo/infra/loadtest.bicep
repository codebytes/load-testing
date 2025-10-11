param loadTestName string
param location string = 'eastus'

resource loadtest 'Microsoft.LoadTestService/loadTests@2023-12-01-preview' = {
  name: loadTestName
  location: location
  identity: {
    type: 'SystemAssigned '
  }
  properties: {
    description: 'demo load test'
  }
}

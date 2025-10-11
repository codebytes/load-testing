param location string = 'eastus'
param webAppName string

var hostingPlanName = '${webAppName}-plan'
var dbAccountName = '${webAppName}-db'

resource webApp 'Microsoft.Web/sites@2023-12-01' = {
  name: webAppName
  location: location
  properties: {
    siteConfig: {
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: Microsoft_Insights_components_webApp.properties.InstrumentationKey
        }
        {
          name: 'WEBSITE_NODE_DEFAULT_VERSION'
          value: '~20'
        }
        {
          name: 'CONNECTION_STRING'
          value: listConnectionStrings('Microsoft.DocumentDb/databaseAccounts/${dbAccountName}', '2015-04-08').connectionStrings[0].connectionString
        }
        {
          name: 'MSDEPLOY_RENAME_LOCKED_FILES'
          value: '1'
        }
      ]
      phpVersion: '7.1'
    }
    serverFarmId: hostingPlan.id
  }
}

resource webAppName_Microsoft_ApplicationInsights_AzureWebSites 'Microsoft.Web/sites/siteextensions@2023-12-01' = {
  parent: webApp
  name: 'Microsoft.ApplicationInsights.AzureWebSites'
}

resource hostingPlan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: hostingPlanName
  location: location
  sku: {
    name: 'P2v3'
    tier: 'PremiumV3'
    size: 'P2v3'
    family: 'Pv3'
    capacity: 1
  }
  kind: 'app'
  properties: {
    perSiteScaling: false
    maximumElasticWorkerCount: 1
    isSpot: false
    reserved: false
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
  }
}

resource databaseAccountId_sampledatabase 'Microsoft.DocumentDB/databaseAccounts/mongodbDatabases@2024-05-15' = {
  parent: databaseAccountId_resource
  name: 'sampledatabase'
  properties: {
    resource: {
      id: 'sampledatabase'
    }
    options: {}
  }
}

resource databaseAccountId_sampledatabase_samplecollection 'Microsoft.DocumentDB/databaseAccounts/mongodbDatabases/collections@2024-05-15' = {
  parent: databaseAccountId_sampledatabase
  name: 'samplecollection'
  properties: {
    resource: {
      id: 'samplecollection'
    }
    options: {}
  }
}

resource databaseAccountId_sampledatabase_samplecollection_default 'Microsoft.DocumentDB/databaseAccounts/mongodbDatabases/collections/throughputSettings@2024-05-15' = {
  parent: databaseAccountId_sampledatabase_samplecollection
  name: 'default'
  properties: {
    resource: {
      throughput: 1000
    }
  }
}

resource Microsoft_Insights_components_webApp 'Microsoft.Insights/components@2020-02-02' = {
  name: webAppName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    Request_Source: 'AzureTfsExtensionAzureProject'
  }
}

resource databaseAccountId_resource 'Microsoft.DocumentDB/databaseAccounts@2024-05-15' = {
  kind: 'MongoDB'
  name: dbAccountName
  location: location
  properties: {
    databaseAccountOfferType: 'Standard'
    locations: [
      {
        locationName: location
      }
    ]
  }
}


const { MongoClient } = require('mongodb');
const connectionData = require('./connectionData.json');

let client;
let connection;

async function Get() {
    if (!connection) {
        const uri = process.env.CONNECTION_STRING;
        if (!uri) throw new Error('CONNECTION_STRING is required');
        const next = new MongoClient(uri);
        client = next;
        connection = next.connect()
            .then(() => next.db(process.env.MONGODB_DATABASE || connectionData.databaseName)
                .collection(process.env.MONGODB_COLLECTION || connectionData.collectionName))
            .catch(async (error) => {
                if (client === next) {
                    client = undefined;
                    connection = undefined;
                }
                await next.close();
                throw error;
            });
    }
    return connection;
}

async function Close() {
    const current = client;
    client = undefined;
    connection = undefined;
    if (current) await current.close();
}

module.exports = { Get, Close };

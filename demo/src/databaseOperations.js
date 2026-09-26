const DbConnection = require('./db');

async function withRetry(operation, retries) {
    for (let attempt = 0; ; attempt++) {
        try {
            return await operation();
        } catch (error) {
            if (attempt >= retries) throw error;
            await new Promise(resolve => setTimeout(resolve, (attempt + 1) * 600));
        }
    }
}

function deliver(promise, callback, errorCallback) {
    return promise.then(callback).catch(error => {
        if (errorCallback) return errorCallback(error);
        throw error;
    });
}

function queryCount(callback, errorCallback, retry = 2) {
    return deliver(withRetry(async () => {
        const collection = await DbConnection.Get();
        return collection.countDocuments({});
    }, retry), callback, errorCallback);
}

function addRecord(pageName, callback, errorCallback, retry = 2) {
    return deliver(withRetry(async () => {
        const collection = await DbConnection.Get();
        await collection.insertMany([{ id: Date.now().toString(), page: pageName }]);
    }, retry), callback, errorCallback);
}

module.exports = { queryCount, addRecord };

const assert = require('node:assert/strict');
const { URL } = require('node:url');
const uri = process.env.CONNECTION_STRING;
assert.ok(uri, 'Set CONNECTION_STRING to the disposable local MongoDB service');
const endpoint = new URL(uri);
assert.ok(['127.0.0.1', 'localhost'].includes(endpoint.hostname), 'Integration tests refuse nonlocal databases');
process.env.MONGODB_DATABASE = `runtime_validation_${process.pid}`;
process.env.MONGODB_COLLECTION = 'visits';
const db = require('../db');
const operations = require('../databaseOperations');

(async () => {
  try {
    const collection = await db.Get();
    let count;
    await operations.queryCount(value => { count = value; });
    assert.equal(count, 0);
    let inserted = false;
    await operations.addRecord('index', () => { inserted = true; });
    assert.equal(inserted, true);
    await operations.queryCount(value => { count = value; });
    assert.equal(count, 1);
    assert.equal((await collection.findOne({ page: 'index' })).page, 'index');
    await collection.drop();
    console.log('Actual application count/insert/read operations passed against disposable MongoDB');
  } finally {
    await db.Close();
  }
})().catch(error => { console.error(error.message); process.exitCode = 1; });

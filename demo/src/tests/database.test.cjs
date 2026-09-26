const test = require('node:test');
const assert = require('node:assert/strict');
const db = require('../db');
const operations = require('../databaseOperations');
const originalGet = db.Get;

test.afterEach(() => { db.Get = originalGet; });

test('queryCount calls countDocuments and delivers the result', async () => {
  db.Get = async () => ({ countDocuments: async filter => {
    assert.deepEqual(filter, {});
    return 3;
  }});
  let count;
  await operations.queryCount(value => { count = value; });
  assert.equal(count, 3);
});

test('addRecord awaits the promise-based insert and completes the callback', async () => {
  let inserted;
  db.Get = async () => ({ insertMany: async rows => { inserted = rows; } });
  let completed = false;
  await operations.addRecord('index', () => { completed = true; });
  assert.equal(completed, true);
  assert.equal(inserted.length, 1);
  assert.equal(inserted[0].page, 'index');
  assert.equal(typeof inserted[0].id, 'string');
});

test('connection errors reach the error callback rather than becoming collection objects', async () => {
  const failure = new Error('synthetic connection failure');
  db.Get = async () => { throw failure; };
  let seen;
  await operations.queryCount(() => assert.fail('unexpected success'), error => { seen = error; }, 0);
  assert.equal(seen, failure);
});

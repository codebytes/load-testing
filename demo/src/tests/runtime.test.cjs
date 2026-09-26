const test = require('node:test');
const assert = require('node:assert/strict');
const { JSDOM } = require('jsdom');
const { MongoClient } = require('mongodb');
const fs = require('node:fs');
const path = require('node:path');

test('jsdom loads and renders the actual application page on the declared runtime', () => {
  const html = fs.readFileSync(path.join(__dirname, '..', 'index.html'), 'utf8');
  const dom = new JSDOM(html);
  const visits = dom.window.document.getElementById('visitCount');
  assert.ok(visits);
  visits.textContent = 'Total visits: 2';
  assert.match(dom.serialize(), /Total visits: 2/);
  dom.window.close();
});

test('the MongoDB client and collection API load without a cloud connection', async () => {
  const client = new MongoClient('mongodb://127.0.0.1:27017');
  const collection = client.db('runtime_validation').collection('visits');
  assert.equal(typeof collection.countDocuments, 'function');
  assert.equal(typeof collection.insertMany, 'function');
  await client.close();
});

---
marp: true
theme: default
footer: 'https://example.com'
mermaid: true
style: |
  .columns {
    display: grid;
    grid-template-columns: repeat(2, minmax(0, 1fr));
    gap: 1rem;
  }
  .columns3 {
    display: grid;
    grid-template-columns: repeat(3, minmax(0, 1fr));
    gap: 1rem;
  } 
  img[alt~="center"] {
    display: block;
    margin: 0 auto;
  }
  .fa-twitter { color: aqua; }
  .fa-mastodon { color: purple; }
  .fa-linkedin { color: blue; }
  .fa-window-maximize { color: skyblue; }

  svg[id^="mermaid-"] { 
    min-width: 480px; 
    max-width: 960px; 
    min-height: 360px; 
    max-height: 600px; 
  }

  @import 'https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.3.0/css/all.min.css'
---

<!-- _footer: 'https://github.com/Codebytes/build-with-bicep' -->

![bg left:40%](./img/background.png)

# Continuous Load Testing with GitHub Actions
![github logo w:200](./img/github-logo.png)

# Chris Ayers ![w:120](img/portrait.png)

---

![bg left:40%](./img/portrait.png)

## Chris Ayers
### Senior Customer Engineer<br>Microsoft

<i class="fa-brands fa-twitter"></i> Twitter: @Chris\_L\_Ayers
<i class="fa-brands fa-mastodon"></i> Mastodon: @Chrisayers@hachyderm.io
<i class="fa-brands fa-linkedin"></i> LinkedIn: - [chris\-l\-ayers](https://linkedin.com/in/chris-l-ayers/)
<i class="fa fa-window-maximize"></i> Blog: [https://chris-ayers\.com/](https://chris-ayers.com/)
<i class="fa-brands fa-github"></i> GitHub: [Codebytes](https://github.com/codebytes)

---

# Continuous Load Testing with GitHub Actions

---

# Agenda

- **What is Load Testing?**
  - **Key Concepts of Load Testing**
  - **Why Load Test?**
- **Manual Testing vs Testing in Pipelines**
  - **Common Load Testing Challenges**
- **JMeter**
  - **JMeter and Azure Load Testing Service**
- **Infrastructure as Code (IaC) and Continuous Load Testing on GitHub Actions**
- **Benefits of Shift-Left Load Testing**
- **Conclusion and Q&A**

--- 

# What is Load Testing?

- Evaluates application, system, or network performance under specific load conditions or increasing levels of load
- Helps identify bottlenecks, ensure reliability, and verify capacity
- Involves simulating real-world usage scenarios and gradually increasing load to observe system behavior

---

# Key Concepts of Load Testing

- **Virtual users:** Simulate concurrent connections to mimic real-world user traffic
- **Ramp-up time:** Time taken to reach the full number of virtual users
- **Response time:** Time from sending a request to receiving the last response
- **Latency:** Time from sending a request to receiving the first response
- **Requests per second (RPS):** Total number of requests generated per second during the test 
> RPS = (number of requests) / (total time in seconds)
> Virtual users = (RPS) * (latency in seconds)

---

# Why Load Test?

- **Identify bottlenecks**: Uncover performance limitations within an application
- **Ensure reliability**: Verify the application can withstand high levels of user traffic
- **Verify capacity**: Validate that the application can handle anticipated user load

---

# What is JMeter?

- Apache open-source software for load testing
- First version dates to 1998
- Runs on Java and supported on all major platforms
- Probably the most popular load testing tool on the market with a huge user community
- Typically, two major releases per year

---

# What can JMeter Do?

- Supports multiple protocols, such as:
  - HTTP
  - SOAP
  - LDAP
  - SMTP
  - JDBC
  - Mutiple other protocols
- Has an IDE for building and debugging load tests (GUI mode)
- Has a CLI for running load tests on scale (non-GUI mode)
- Integrated reporting and analysis tools

---

# JMeter Limitations

- JMeter is NOT a browser!
- It does not execute any JavaScript code or evaluate any HTML or CSS!
- Therefore, the Response Time in JMeter will be less than the actual response time in a browser!

---

# Manual Testing vs Testing in Pipelines

- Manual testing: Time-consuming, prone to human error, and inconsistent
- Testing in pipelines: Automated, consistent, scalable, and integrated with development processes

---

# Typical Problems with Load Testing

- Insufficient test coverage
- Last-minute testing leading to delays and increased costs
- Inaccurate load simulation
- Lack of integration with development processes

---

# JMeter and Azure Load Testing Service

- JMeter: Open-source, extensible load testing tool
- Azure Load Testing Service: Scalable, cloud-based platform for running JMeter tests
- Combines the power of JMeter with the scalability and reliability of Azure

---

# Creating JMeter Tests

- Plan your test scenario
- Record user interactions using JMeter's proxy
- Customize test parameters and settings
- Validate and run the test

---

# JMeter Workflow

![w:900px](img/jmeter-workflow.drawio.png)

---

# Azure Load Testing Service
![w:900px](img/azure-load-testing-architecture.png)

---

# Utilizing Azure Load Testing Service

- Upload JMeter test plan to Azure
- Configure test duration, load pattern, and number of users
- Monitor test progress and analyze results

---

# Infrastructure as Code (IaC)

- IaC: Managing and provisioning infrastructure through code
- Enhances repeatability, consistency, and scalability
- Popular tools: Terraform, ARM Templates, Bicep Templates

---

# Continuous Load Testing on GitHub Actions

- GitHub Actions: Platform for automation and CI/CD
- Automate load testing as part of your development process
- Run tests against ephemeral environments created using IaC

---

# Benefits of Shift-Left Load Testing

- Early identification of performance bottlenecks
- Improved collaboration between developers, testers, and operations
- Reduced costs and increased confidence in solutions
- Faster feedback loop and shorter time to market

---

# Demos

---

# Questions

![bg right](./img/owl.png)

---

# Resources 


<div class="columns">
<div>

## Links

- [https://docs.microsoft.com/en-us/events/learntv/learnlive-iac-and-bicep/](https://docs.microsoft.com/en-us/events/learntv/learnlive-iac-and-bicep/)
- [https://github.com/codebytes](https://github.com/codebytes)

</div>
<div>

## Chris Ayers 

<i class="fa-brands fa-twitter"></i> Twitter: @Chris\_L\_Ayers
<i class="fa-brands fa-mastodon"></i> Mastodon: @Chrisayers@hachyderm.io
<i class="fa-brands fa-linkedin"></i> LinkedIn: - [chris\-l\-ayers](https://linkedin.com/in/chris-l-ayers/)
<i class="fa fa-window-maximize"></i> Blog: [https://chris-ayers\.com/](https://chris-ayers.com/)
<i class="fa-brands fa-github"></i> GitHub: [Codebytes](https://github.com/codebytes)

</div>

</div>

<script type="module">
  import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.esm.min.mjs';
  mermaid.initialize({ startOnLoad: true });
</script>
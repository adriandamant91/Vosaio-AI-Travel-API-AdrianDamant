1. Code & Architecture 
● How does your solution follow SOLID principles? 
We can discuss this in our follow-up but to the best of my knowledge SOLID principles are being obeyed in this project.
● How did you structure your API (Controllers, Services, Repositories)? 
Because it was quite a simple api user-facing project I have a single controller to handle the single user input event. The business logic as well as the ChatGPT integration is all managed within a GETS project within the solution, with a project reference allowing the logic to be used within the main solution housing the public api endpoint.
Dependence injection insures we only have a single instance of the needed services and that they are neatly injected into the controller and classes they are using in.

2. AI Integration 
● Which AI model did you use, and why? 
I integrated in ChatGPT as I had  not used this model before and was curios to give a test and learn more about it and see how it handled structured data returns.
It does require well structure prompt to get the model to return the structured data I require.

● How does your API process user input to generate an itinerary? 
I setup an Enum to manage the data return types which is linked to a dictionary where the explanation instructions are house which tell ChatGPT very clearly what to do. To ensure I receive structured data back I serialise the return classes and include the Json text within the prompt. 

using the Enum and dictionary link all the "wordy" instructions are housed within the Constants class, ensuring the solution is easily maintainable and scalable.

3. Error Handling & Validation 
● How does your API handle incorrect or missing user input? 
I have custom error handling which looks for exceptions, returning an industry standard error code along with a description from within the API. You will see this used throughout the solution.
● What are the key edge cases you considered? 
I did not have enough time to dedicate to advanced unit testing for this project I am afraid, and I hope the work I have done will showcase my abilities without me needing to spend another few evenings on the solution. Unit testing is quite time consuming. The tests I did include are testing business logic, such as date calculations and processing the users input data. 
4. Performance & Scalability 
● How does your API handle multiple concurrent requests? 
Asynchronously, however I have not limited to concurrent connections to critical resources in this solution, more advanced thread management will need to be added to the project to make it more scalable and capable of handling more concurrent connections and users. Semaphore comes to mind.
● What would you change to improve scalability? 
Better concurrent thread handling and thread limitations on the ChatGPT service.
5. Additional Innovation (Bonus Points) 
● Did you implement any additional features or optimizations? 
I think I went above and beyond and have produced a functional and useful starting point for such an itinerary solution, the idea was to showcase my knowledge and technical skills, as well as evaluate my code to ensure it follows best practices and is easy for other developers to understand and to maintain and I hope that has been achieved.
● How does your solution stand out from a standard AI itinerary generator?
If I had more time to give to this project there is a lot of ideas we can include, happy to discuss these in the follow-up.
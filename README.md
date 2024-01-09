Zoo Animal Management System
Description: You are assigned to develop an Animal Transfer System in .NET
that facilitates the movement of animals to an empty zoo. The system should 
consider specific rules and guidelines to ensure the appropriate grouping of 
animals during the transfer process. 
Features:
Implement a mechanism for zookeepers to accommodate animals from the input 
JSON to the new zoo.

o The system should take into account the list of animals provided
and the number of new enclosures available in the new zoo. All 
animals should fit.

o Animals should be transferred while adhering to the following rules:

	▪ Vegetarian animals can be placed together in the same 
	enclosure. 
	▪ Animals of the same species should not be separated and 
	should be assigned to the same enclosure. 
	▪ Meat-eating animals of different species should preferably not 
	be grouped together in the same enclosure. However, if 
	necessary due to limited enclosures, only two different species 
	of meat-eating animals can be grouped together.
	▪ Additional rules : 
		Only assign if enclosure size is Huge and theres is less than 10 animals or if large and theres less than 8 animals
	    Only assign if there are less than 2 species in the enclosure or if enclosure is small allow only 1 species and only if enclosures are empty or there is no herbivore
					
Input:

JSON (animals.json) full of animals with their specifications (food, 
species...)

JSON (enclosures.json) full of enclosures with their specifications 
(inside/outside, size, objects...)

Output:

Database filled with all the animals from the JSON. Each animal should 
have an enclosure assigned to him.


Firstly You need  to upload files using:
   	/api/ZooAnimalFileUpload/uploadEnclosures - upload enclosures.json
   	/api/ZooAnimalFileUpload/uploadAnimals - upload animals.json
Secondly you can use:
	/api/ZooAnimalManager/FillEnclosures - to automatically assign animals to enclosures based on rules.
Other functions also works.

![image](https://github.com/karka1234/Zoo-Animal-Management-System/assets/5184302/33ebea5c-f1a0-4ac7-98e5-7f735a9f756c)

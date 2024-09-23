# code-voyage
 a ship needs to get home or something else

using the relationship model from the previous code.

you are now on a ship

you have three things

a map: this map displays every planet. this can be done crudely. I thing. lets use a screen and that there is a character on each block

travel: you can travel between each planet

during these travels things can happen. these events can be based on
- the map itself
- map updates one get from other planets.
- info from templates (books) found
- people on your ship

may be complicated, but when going from plannet to plabet, you get the information you gathered.
you can go through this information and see if its good. or, because this is in real time, go and then maybe find out there is a backhole
your choice

choices are important. each planet has a resource. you an gather it, although there are choices.
- is the planet inhabited
- do you have permission
- do you have the tools

to play it safely. tools can be created, but they take time (no resources needed to make them)
if inhabited, you have a diplomacy. it needs to fill up. success gives you exp, not successful lowers your chances
if its full, congrats, you get the stuff. if not, war (war needs to be implimented)
there is also the choice of giving them something, this again, can take time.

planets are usefull, they give you
- resources
- people
- information

there are passengers on your ship: 

user
- name
- last name
- rank
- health
- stamina
- place of origin
- space needed
- birthday
- combat style
- speed
- happiness
- hunger
- location
- location bed

the ship: the ship has certain compartments. each day a person goes to
- a compartment based on their proffesion
- they have a room to sleep
- the captain is always on the bridge
- the chief engineer in engineering
- scientist can either be experimenting, on the bridge
- other compartments can be designated. based on the people you have on the bridge

- now. the only thing now is to have them move. each day they move from bed -> breakfast -> work -> lunch -> work -> dinner -> bed

things to add

- cycles. not everyone works. so there is a 12 hour shift. but not every proffession needs it
- captain
- engineer
- cook

thats it.

proffesion:
there is also a
- captain
- cook
- chief engineer
- chief scientist

so those beds are there.

then a random assortment of people. each giving you a plus and a minus to the chip itself. you need to work with what you have.

with resources gathered you can expand the bridge and then on planets give them in.

there are three types of rooms

- sleep
- function
- entertainment (eating)

while entertainments can be increased (so much space per person. if its lower, they get unhappy)
sleep for each perosn and their needs
function, their proffession requires X space

year only has 365 days;

the User {
    string name;
    string last name
    string rank
    string proffession;
    int health
    int stamina
    string place of origin
    string space needed
    string birthday
    string combat style
    int speed
    int happiness
    int hunger
    int location user;
    string age in days
    int location sleep;
    int location work;

}
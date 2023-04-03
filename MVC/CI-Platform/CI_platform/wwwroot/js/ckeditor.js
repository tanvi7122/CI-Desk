////////// script of ckeditor
////let optionsButtons = document.querySelectorAll(".option-button");
////let writingArea = document.getElementById("text-input");
////let formatButtons = document.querySelectorAll(".format");
////let scriptButtons = document.querySelectorAll(".script");


////// Initial Setting
////const initializer = () => {
////    highlighter(formatButtons,false);
////    highlighter(scriptButtons,true);
////};

////// main logic
////const modifyText = (command,defaultUi,value) => {
////    document.execCommand(command,defaultUi,value);
////};

////// button operations
////optionsButtons.forEach(button => {
////    button.addEventListener("click", () => {
////        modifyText(button.id,false,null);
////    });
////});

////// function format(){
//////     var id = document.getElementById("textformat");
//////     id.style.textDecoration="none";
////// }


////// function for highlight selected options
////const highlighter = (className , needsRemoval) => {
////    className.forEach((button) => {
////        button.addEventListener("click",() => {
////             if(needsRemoval){
////                let alreadyActive = false;

////                // clicked button is active
////                if(button.classList.contains("active")){
////                    alreadyActive = true;
////                }

////                highlighterRemover(className);
////                if(!alreadyActive){
////                    // highlight clicked button
////                    button.classList.add("active");
////                }
////            }
////            else{
////                // other button can highlighted
////                button.classList.toggle("active");
////            }
////        });
////    });
////};

////// remove highlighter
////const highlighterRemover = (className) => {
////    className.forEach((button) => {
////        button.classList.remove("active");
////    });
////}



////window.onload = initializer();


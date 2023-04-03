//// share story 
////let files = [],
////dragarea = document.queryselector('.drag-area'),
////input = document.queryselector('.drag-area input'),
////button = document.queryselector('.drag-card button'),
////select = document.queryselector('.drag-area .select'),
////container = document.queryselector('.container-img');

////* click listener 
////select.addeventlistener('click', () => input.click());

//// input change event 
////input.addeventlistener('change', () => {
////	let file = input.files;
        
////     if user select no image
////    if (file.length == 0) return;
         
////	for(let i = 0; i < file.length; i++) {
////        if (file[i].type.split("/")[0] != 'image') continue;
////        if (!files.some(e => e.name == file[i].name)) files.push(file[i])
////    }

////	showimages();
////});

////* show images 
////function showimages() {
////	container.innerhtml = files.reduce((prev, curr, index) => {
////		return `${prev}
////		    <div class="image">
////			    <span onclick="delimage(${index})">&times;</span>
////			    <img src="${url.createobjecturl(curr)}" />
////			</div>`
////	}, '');
////}

//// delete image 
////function delimage(index) {
////   files.splice(index, 1);
////   showimages();
////}

//// drag & drop 
////dragarea.addeventlistener('dragover', e => {
////	e.preventdefault()
////	dragarea.classlist.add('dragover')
////})

//// drag leave 
////dragarea.addeventlistener('dragleave', e => {
////	e.preventdefault()
////	dragarea.classlist.remove('dragover')
////});

//// drop event 
////dragarea.addeventlistener('drop', e => {
////	e.preventdefault()
////    dragarea.classlist.remove('dragover');

////	let file = e.datatransfer.files;
////	for (let i = 0; i < file.length; i++) {
////		* check selected file is image 
////		if (file[i].type.split("/")[0] != 'image') continue;
		
////		if (!files.some(e => e.name == file[i].name)) files.push(file[i])
////	}
////	showimages();
////});


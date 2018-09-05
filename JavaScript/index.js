function append_last(generatedStringsList, keyword) {
	generatedStringsList.push(keyword);
	return generatedStringsList;
}

function exclusive_case(generatedStringsList, keyword) {
    return [keyword];
}

function find_index_char_begin_in_str_array(array, char) {
	return array.findIndex(value =>
		value.charAt(0) === char);
}

function append_before_B(generatedStringsList, keyword) {
	let index_B = find_index_char_begin_in_str_array(generatedStringsList, 'B');
	if (index_B >= 0) {
        generatedStringsList.splice(index_B, 0, keyword);
        return generatedStringsList;
	}
	return append_last(generatedStringsList, keyword);
}

function reverse(generatedStringsList, keyword) {
	generatedStringsList.reverse();
	return generatedStringsList;
}

// play the game in specified range (inclusive)
function the_game(minNum, maxNum) {
    let rules = {
    	3:[append_last, 'Fizz'],
        5:[append_last, 'Buzz'],
        7:[append_last, 'Bang'],
        11:[exclusive_case, 'Bong'],
        13:[append_before_B,'Fezz'],
        17:[reverse, 'N/A']
    };

    for (let i = minNum; i <= maxNum; i++) {
        let str_list = [];
        // for each number applies all the suitable rules
        for (let key in rules) {
            // check if the property/key is defined in the object itself, not in parent
            if (rules.hasOwnProperty(key)) {
                if (i % key === 0)
                    str_list = rules[key][0](str_list, rules[key][1]);
                //console.log(key, rules[key](str, 'foo'));
            }
        }
        // if no rules has been applied, the number itself should be printed
        if (str_list.length === 0){
            str_list.push(i.toString());
        }
        console.log(str_list.join(''));
    }
}

const readLine = require('readline');

const rl = readLine.createInterface({
    input: process.stdin,
    output: process.stdout
});

rl.question('Please enter maximum number for extended FizzBuzz game: ', (answer) => {
    console.log(`Thank you for your valuable feedback: ${answer}`);
    the_game(1, parseInt(answer));
    rl.close();
});
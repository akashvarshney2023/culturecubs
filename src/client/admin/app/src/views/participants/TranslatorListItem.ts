import type { CandidateDto } from "@/api/candidate";
import type { Contest } from "@/api/microsite";

// Define a TypeScript class for translation
export class CandidateTranslator {
    private contestMap: Record<string, string>; // Mapping of contestId to contestName
  
    constructor(contestList: Contest[]) {
      this.contestMap = this.createContestMap(contestList);
    }
  
    private createContestMap(contestList: Contest[]): Record<string, string> {
      const map: Record<string, string> = {};
      for (const contest of contestList) {
        map[contest.id] = contest.title;
      }
      return map;
    }
  
    translate(candidateList: CandidateDto[]): CandidateListItem[] {
      return candidateList.map((candidate) => {
        const translatedItem: CandidateListItem = {
          name: candidate.name,
          email: candidate.email,
          phone: candidate.phone,
          contest: this.contestMap[candidate.contestId] || 'Unknown Contest',
          attachment:candidate.attachment,
          //actions: candidate.actions,
          raw: candidate, // You can include the raw candidate data if needed
        };
        return translatedItem;
      });
    }
  }
  
  // Define the structure for the translated list item
  interface CandidateListItem {
    name: string;
    email: string;
    phone: string;
    contest: string;
    actions: string; // Assuming this is a string, update it based on your data structure
    raw: CandidateDto; // Include raw candidate data if needed
  }
  